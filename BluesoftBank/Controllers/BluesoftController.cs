using BluesoftBank.Data;
using BluesoftBank.Dto;
using BluesoftBank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BluesoftController : Controller
    {

        private readonly ClienteDbContext _clienteContext;
        private readonly CuentaDbContext _cuentaContext;
        private readonly MovimientoDbContext _movimientoContext;

        public BluesoftController(ClienteDbContext clienteContext, CuentaDbContext cuentaContext, MovimientoDbContext movimientoContext)
        {
            _clienteContext = clienteContext;
            _cuentaContext = cuentaContext;
            _movimientoContext = movimientoContext;
        }

        [HttpGet("saldo/{idCuenta}")]
        public IActionResult ConsultarSaldo(long idCuenta)
        {
            var cuenta = _cuentaContext.Cuentas.Find(idCuenta);

            if (cuenta == null)
            {
                return NotFound("Cuenta no encontrada");
            }

            return Ok(new { Saldo = cuenta.Saldo });
        }

        [HttpGet("movimientos/{idCuenta}")]
        public IActionResult ConsultarMovimientos(long idCuenta)
        {
            var movimientos = _movimientoContext.Movimientos
                .Where(m => m.IdCuenta == idCuenta)
                .OrderByDescending(m => m.FechaMovimiento)
                .ToList();

            return Ok(movimientos);
        }

        [HttpPost("crearCliente")]
        public IActionResult CrearCliente([FromBody] ClienteConsignacionDto clienteDto)
        {
            using (var transaction = _clienteContext.Database.BeginTransaction())
            {
                try
                {
                    
                    var cuenta = new Cuenta
                    {
                        Numero = GenerarNumeroCuenta(),
                        Tipo = TipoCuenta.Ahorro, 
                        Saldo = clienteDto.MontoConsignacion, 
                        FechaApertura = DateTime.Now,
                        CiudadCuenta = clienteDto.Dirección, 
                    };

                    _cuentaContext.Cuentas.Add(cuenta);
                    _cuentaContext.SaveChanges();

                    
                    var cliente = new Cliente
                    {
                        Nombres = clienteDto.Nombres,
                        Apellidos = clienteDto.Apellidos,
                        Dirección = clienteDto.Dirección,
                        Telefono = clienteDto.Telefono,
                        IdCuenta = cuenta.IdCuenta
                    };

                    _clienteContext.Clientes.Add(cliente);
                    _clienteContext.SaveChanges();

                    transaction.Commit();

                    return Ok("Cliente y cuenta creados exitosamente");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, "Error al crear cliente y cuenta: " + ex.Message);
                }
            }
        }

        [HttpGet("clientesTransaccionesPorMes/{mes}")]
        public IActionResult ObtenerClientesTransaccionesPorMes(int mes)
        {
            var cuentasConMovimientos = _movimientoContext.Movimientos
                .Where(m => m.FechaMovimiento.Month == mes)
                .Select(m => m.IdCuenta)
                .Distinct()
                .ToList();

            var clientesConTransacciones = _clienteContext.Clientes
                .Where(c => cuentasConMovimientos.Contains(c.IdCuenta))
                .Select(c => new
                {
                    Cliente = new
                    {
                        IdCliente = c.IdCliente,
                        Nombres = c.Nombres,
                        Apellidos = c.Apellidos,
                        Dirección = c.Dirección,
                        Telefono = c.Telefono,
                        NumTransacciones = cuentasConMovimientos.Count(id => id == c.IdCuenta)
                    }
                })
                .OrderByDescending(c => c.Cliente.NumTransacciones)
                .ToList();

            return Ok(clientesConTransacciones);
        }


        private string GenerarNumeroCuenta()
        {
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString();
        }

        [HttpPost("realizarMovimiento")]
        public IActionResult RealizarMovimiento([FromBody] MovimientoDto movimientoDto)
        {
            if (movimientoDto.Tipo != TipoMovimiento.Retiro && movimientoDto.Tipo != TipoMovimiento.Consignacion)
            {
                return BadRequest("El tipo de movimiento debe ser 0 (Retiro) o 1 (onsignacion)");
            }

            using (var transaction = _cuentaContext.Database.BeginTransaction())
            {
                try
                {
                    var cuenta = _cuentaContext.Cuentas.Find(movimientoDto.IdCuenta);
                    if (cuenta == null)
                    {
                        return NotFound("Cuenta no encontrada");
                    }

                    if (movimientoDto.Tipo == TipoMovimiento.Retiro && !PuedeRealizarRetiro(cuenta, movimientoDto.Valor))
                    {
                        return BadRequest("Saldo insuficiente para realizar el retiro");
                    }

                    var movimiento = new Movimiento
                    {
                        IdCuenta = movimientoDto.IdCuenta,
                        Tipo = movimientoDto.Tipo,
                        Valor = movimientoDto.Valor,
                        FechaMovimiento = DateTime.Now,
                        CiudadMovimiento = movimientoDto.CiudadMovimiento
                    };

                    _movimientoContext.Movimientos.Add(movimiento);
                    _movimientoContext.SaveChanges();

                    ActualizarSaldoCuenta(cuenta, movimientoDto.Tipo, movimientoDto.Valor);
                    _cuentaContext.SaveChanges();

                    transaction.Commit();

                    return Ok("Movimiento realizado exitosamente");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, "Error al realizar el movimiento: " + ex.Message);
                }
            }
        }

        private bool PuedeRealizarRetiro(Cuenta cuenta, decimal valorRetiro)
        {
            return cuenta.Saldo >= valorRetiro;
        }

        private void ActualizarSaldoCuenta(Cuenta cuenta, TipoMovimiento tipoMovimiento, decimal valor)
        {
            if (tipoMovimiento == TipoMovimiento.Retiro)
            {
                cuenta.Saldo -= valor;
            }
            else
            {
                cuenta.Saldo += valor;
            }
        }
    }
}
