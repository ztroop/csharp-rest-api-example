using AutoMapper;
using CSharpRESTDemo.Dtos;
using CSharpRESTDemo.Entities;
using CSharpRESTDemo.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSharpRESTDemo.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private IMapper _mapper;
        private ICustomerRepository _customerRepository;

        public CustomersController(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var allCustomers = _customerRepository.GetAll().ToList();

            var allCustomersDto = allCustomers.Select(x => _mapper.Map<CustomerDto>(x));

            return Ok(allCustomersDto);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSingleCustomer")]
        public IActionResult GetSingleCustomer(Guid id)
        {
            Customer customerFromRepo = _customerRepository.GetSingle(id);

            if (customerFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customerFromRepo));
        }

        // POST api/customers

        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerCreateDto customerCreateDto)
        {
            Customer toAdd = _mapper.Map<Customer>(customerCreateDto);

            _customerRepository.Add(toAdd);

            bool result = _customerRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return CreatedAtRoute("GetSingleCustomer", new { id = toAdd.Id }, _mapper.Map<CustomerDto>(toAdd));
        }

        // PUT api/customers/{id}

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCustomer(Guid id, [FromBody] CustomerUpdateDto updateDto)
        {
            var existingCustomer = _customerRepository.GetSingle(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            _mapper.Map(updateDto, existingCustomer);

            _customerRepository.Update(existingCustomer);

            bool result = _customerRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(_mapper.Map<CustomerDto>(existingCustomer));
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult PartiallyUpdate(Guid id, [FromBody] JsonPatchDocument<CustomerUpdateDto> customerPatchDoc)
        {
            if (customerPatchDoc == null)
            {
                return BadRequest();
            }

            var existingCustomer = _customerRepository.GetSingle(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            var customerToPatch = _mapper.Map<CustomerUpdateDto>(existingCustomer);
            customerPatchDoc.ApplyTo(customerToPatch);

            _mapper.Map(customerToPatch, existingCustomer);

            _customerRepository.Update(existingCustomer);

            bool result = _customerRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(_mapper.Map<CustomerDto>(existingCustomer));
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(Guid id)
        {
            var existingCustomer = _customerRepository.GetSingle(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(id);

            bool result = _customerRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return NoContent();
        }

    }
}