using System;
using Microsoft.AspNetCore.Mvc;
using CategoryService.Service;
using CategoryService.Models;
using CategoryService.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
namespace CategoryService.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService service;
        public CategoryController(ICategoryService _service)
        {
            this.service = _service;
        }
        /// <summary>
        /// Get Category details by Id.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{categoryId:int}")]
        public IActionResult Get(int categoryId)
        {
            try
            {
                return Ok(service.GetCategoryById(categoryId));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Unable to fetch the Category details");
            }
        }
        /// <summary>
        /// Get all the categories created by a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            try
            {
                return Ok(service.GetAllCategoriesByUserId(userId));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.Unable to fetch the Category details");
            }
        }
        /// <summary>
        /// Create a new Category.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            try
            {
                return Created("", service.CreateCategory(value));
            }
            catch (CategoryNotCreatedException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Unable to create new category");
            }
        }
        /// <summary>
        /// Update the existing category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
        {
            try
            {
                var updateResult = service.UpdateCategory(id, value);
                if (updateResult == true)
                    return Ok(updateResult);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Unable to update the category");
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Unable to update the category");
            }
        }
        /// <summary>
        /// Delete the Category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeleteCategory(id));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Unable to delete the category");
            }
        }
    }
}