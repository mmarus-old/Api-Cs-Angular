﻿using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerosController : ControllerBase
    {
        private readonly HeroService _heroService;

        public HerosController(HeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet("{id:length(24)}", Name = "GetHero")]
        public ActionResult<Hero> GetById(string id)
        {
            var hero = _heroService.Get(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }

        [HttpGet]
        public ActionResult<List<Hero>> SearchByName([FromQuery] string name) =>
             string.IsNullOrEmpty(name) ? _heroService.Get() : _heroService.SearchByName(name);


        [HttpPost]
        public ActionResult<Hero> Create(Hero hero)
        {
            _heroService.Create(hero);

            return CreatedAtRoute("GetHero", new { id = hero.Id.ToString() }, hero);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Hero heroIn)
        {
            var hero = _heroService.Get(id);

            if (hero == null)
            {
                return NotFound();
            }

            _heroService.Update(id, heroIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var hero = _heroService.Get(id);

            if (hero == null)
            {
                return NotFound();
            }

            _heroService.Remove(hero.Id);

            return NoContent();
        }
    }
}
