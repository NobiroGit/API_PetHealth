using Microsoft.AspNetCore.Mvc;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Medicines;
using PetHealth.Application.Repositories;
using PetHealth.Domain.Entities;

namespace API_PetHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicineController : Controller
{
    private readonly IMedicineRepository _medicineRepository;

    public MedicineController(IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    #region GET

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<Medicine>>>> GetAllMedicines()
    {
        Result<IEnumerable<Medicine>> medicines = await _medicineRepository.Execute(new GetAllMedicineQueryAsync());
        if (!medicines.IsSuccess) return BadRequest();
        return Ok(medicines.Data);
    }

    #endregion
}