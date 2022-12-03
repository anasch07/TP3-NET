using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers;


public class PersonController : Controller
{

    [Route("Person/{id:int}")]
    public IActionResult View(int id)
    {
        Debug.WriteLine("ID: " + id.ToString());
        Person? p = PersonalInfo.GetPerson(id);
        if (p != null)
        {
            ViewBag.person = p;
            return View();
        }

        ViewBag.error = "Not found";
        return View("ErrorAction");
    }


    [Route("Person/")]
    public IActionResult All()
    {
        List<Person> res = PersonalInfo.GetAllPerson();
        return View(res);
    }
    
    
    [Route("Person/Search")]
    [HttpGet]
    public IActionResult Search()
    {
        ViewBag.notFound = false;
        return View();
    }

    [Route("Person/Search")]
    [HttpPost]
    public IActionResult Search(String first_name, String country)
    {
        Person person = PersonalInfo.GetPersonByFirstNameAndCountry(first_name, country);
        if (person != null)
        {
            return Redirect(person.id.ToString());
        }
        ViewBag.notFound = true;
            return View();
    }


}


