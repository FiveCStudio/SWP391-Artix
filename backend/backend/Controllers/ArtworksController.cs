﻿using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;

[ApiController]
[Route("api/[controller]")]
public class ArtworksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ArtworksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Artworks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Artworks>>> GetArtworks()
    {
        var artworks = await _context.Artworks
     .Select(a => new Artworks
     {
         // Assuming Id is the problematic Int32 property, handle NULL with null-conditional operator
         ArtworkID = a.ArtworkID,
         CreatorID = a.CreatorID,
         TagID = a.TagID,
         ArtworkName = a.ArtworkName,
         Description = a.Description,
         DateCreated = a.DateCreated,
         Likes = a.Likes,
         Purchasable = a.Purchasable,
         Price = a.Price,
         ImageFile = a.ImageFile != null ? (IFormFile)a.ImageFile : null,

     })
     .ToListAsync();

        return artworks;

    }

    // GET: api/Artworks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Artworks>> GetArtwork(int id)
    {
        var artwork = await _context.Artworks
            .Select(a => new Artworks
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                TagID = a.TagID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ImageFile = a.ImageFile != null ? (IFormFile)a.ImageFile : null,
            })
            .FirstOrDefaultAsync(a => a.ArtworkID == id);

        if (artwork == null)
        {
            return NotFound();
        }

        return artwork;
    }
    // GET: api/Artworks/ByCreator/{Crid}
    [HttpGet("ByCreator/{Crid}")]
    public async Task<ActionResult<IEnumerable<Artworks>>> GetArtworkByCreatorID(int Crid)
    {
        var artworks = await _context.Artworks
            .Where(a => a.CreatorID == Crid)
            .Select(a => new Artworks
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                TagID = a.TagID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ImageFile = a.ImageFile != null ? (IFormFile)a.ImageFile : null,
            })
            .ToListAsync();

        if (artworks == null || artworks.Count == 0)
        {
            return NotFound();
        }

        return artworks;
    }


    // POST: api/Artworks
    [HttpPost]
    public async Task<ActionResult<Artworks>> PostComment(Artworks Artwork)
    {
        _context.Artworks.Add(Artwork);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArtwork), new { id = Artwork.ArtworkID }, Artwork);
    }


    // PUT: api/Artworks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutArtwork(int id, Artworks artwork)
    {
        if (id != artwork.ArtworkID)
        {
            return BadRequest();
        }

        _context.Entry(artwork).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArtworkExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Artworks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtwork(int id)
    {
        var artwork = await _context.Artworks.FindAsync(id);

        if (artwork == null)
        {
            return NotFound(); // Return 404 if the artwork is not found
        }

        try
        {
            //artwork.ImageFile = artwork.ImageFile ?? Array.Empty<IFormFile>();

            _context.Artworks.Remove(artwork);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content upon successful deletion
        }
        catch (Exception ex)
        {
            // Handle any exception that might occur during deletion
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }


    private bool ArtworkExists(int id)
    {
        return _context.Comments.Any(e => e.CommentID == id);
    }



    [HttpGet("TopLiked")]
    public async Task<ActionResult<IEnumerable<Artworks>>> GetTopLikedArtworks()
    {
        var topLikedArtworks = await _context.Artworks
            .OrderByDescending(a => a.Likes)
            .Take(10)
            .Select(a => new Artworks
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                TagID = a.TagID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ImageFile = a.ImageFile != null ? (IFormFile)a.ImageFile : null,
            })
            .ToListAsync();

        if (topLikedArtworks == null || topLikedArtworks.Count == 0)
        {
            return NotFound();
        }

        return topLikedArtworks;
    }

}