using AutoMapper;
using Domain.Models;
using Domain.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;
        public BooksController(IRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            var books = await _repository.GetAllAsync();
            return Ok(_mapper.Map<List<BookDTO>>(books));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var books = await _repository.GetAsync(id);
            if (books == null) return NotFound("There is no book with this id.");
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            bool result = await _repository.AddAsync(book);
            if (!result) return BadRequest("Something went wrong :(");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]BookDTO bookDTO)
        {
            Book existBook = await _repository.GetAsync(id);
            if (existBook == null) return NotFound("There is no book with this id.");

            existBook.Title = bookDTO.Title;
            existBook.Author = bookDTO.Author;
            existBook.Genre = bookDTO.Genre;
            existBook.ReleaseDate = bookDTO.ReleaseDate;

            bool result = _repository.Update(existBook);
            if (!result) return BadRequest("Something went wrong :(");
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
