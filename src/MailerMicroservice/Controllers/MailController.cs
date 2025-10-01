// src/MailerMicroservice/Controllers/MailController.cs
using MailerMicroservice.Models;
using MailerMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailerMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly MailService _mailService;

    public MailController(MailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromBody] MailRequest request)
    {
        if (
            string.IsNullOrWhiteSpace(request.To)
            || string.IsNullOrWhiteSpace(request.Subject)
            || string.IsNullOrWhiteSpace(request.Body)
        )
        {
            return BadRequest(new { message = "To, Subject and Body are required." });
        }

        await _mailService.SendMailAsync(request);
        return Ok(new { message = "Mail sent successfully." });
    }
}
