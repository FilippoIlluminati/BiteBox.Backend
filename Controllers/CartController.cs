using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiteBox.Backend.Entities;
using BiteBox.Backend.Dtos;


namespace BiteBox.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CartController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Recupera il carrello dell'utente autenticato
        [HttpGet]
        public IActionResult GetCart()
        {
            var userId = User.Identity?.Name;
            if (userId == null) return Unauthorized();

            var cart = _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(cr => cr.MenuItem)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _dbContext.Carts.Add(newCart);
                _dbContext.SaveChanges();
                return Ok(newCart);
            }

            return Ok(cart);
        }

        // Aggiunge un elemento al carrello
        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] CartItemCreationDto cartRow)
        {
            var userId = User.Identity?.Name;
            if (userId == null) return Unauthorized();

            var cart = _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Carrello non trovato.");
            }

           
            var newCartItem = new CartItem
            {
                CartId = cart.Id,
                MenuItemId = cartRow.MenuItemId,
                Quantity = cartRow.Quantity,
            };
            _dbContext.CartItems.Add(newCartItem);
            _dbContext.SaveChanges();

            return Ok(cart);
        }
        

        // aggiorna la quantità di un elemento nel carrello
        [HttpPut("update")]
        public IActionResult UpdateCartRow([FromBody] CartItemUpdateDto updatedRow)
        {
            var userId = User.Identity?.Name;
            if (userId == null) return Unauthorized();

            var existingRow = _dbContext.CartItems
                .Include(cr => cr.Cart)
                .FirstOrDefault(cr => cr.Id == updatedRow.Id && cr.Cart.UserId == userId);

            if (existingRow == null) return NotFound("Riga del carrello non trovata.");

            existingRow.Quantity = updatedRow.Quantity;
            _dbContext.SaveChanges();

            return Ok(existingRow);
        }

        // Rimuove un elemento dal carrello
        [HttpDelete("remove/{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            var userId = User.Identity?.Name;
            if (userId == null) return Unauthorized();

            var cartItem = _dbContext.CartItems
                .Include(cr => cr.Cart)
                .FirstOrDefault(cr => cr.Id == id && cr.Cart.UserId == userId);

            if (cartItem == null) return NotFound("Riga del carrello non trovata.");

            _dbContext.CartItems.Remove(cartItem);
            _dbContext.SaveChanges();

            return Ok("Elemento rimosso dal carrello.");
        }
    }
}
