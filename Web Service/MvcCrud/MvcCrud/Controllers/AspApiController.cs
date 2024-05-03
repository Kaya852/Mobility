using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCrud.Models;

namespace MvcCrud.Controllers
{
    public class AspApi : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AspApi(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: Records
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync("http://localhost:5100/api/Records/list");

                if (response.IsSuccessStatusCode)
                {
                    var records = await response.Content.ReadFromJsonAsync<List<Record>>();

                    // Filter records based on searchString
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        records = records.Where(r =>
                            r.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            r.Surname.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        // Add more properties if needed for filtering
                        ).ToList();
                    }

                    return View(records);
                }
                else
                {
                    return Problem($"Server error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Client error: {ex.Message}");
            }
        }

        // GET: Records/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Records/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Age")] Record record)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://localhost:5100/api/Records/add", record);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Problem($"Server error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Client error: {ex.Message}");
            }
        }

        // GET: Records/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.DeleteAsync($"http://localhost:5100/api/Records/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Problem($"Server error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Client error: {ex.Message}");
            }
        }
    }
}
