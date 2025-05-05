using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;
using ShopMax.MVC.Models;

namespace ShopMax.MVC.Controllers;

public class HomeController(
	ILogger<HomeController> logger,
	IMapper mapper,
	IProductService productService
	) : Controller
{
	private readonly ILogger<HomeController> _logger = logger;
	private readonly IMapper _mapper = mapper;
	private readonly IProductService _productService = productService;

	public async Task<IActionResult> IndexAsync()
	{
		return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productService.GetAllWithCategory()));
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
