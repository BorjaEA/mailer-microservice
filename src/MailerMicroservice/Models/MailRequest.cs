// src/MailerMicroservice/Models/MailRequest.cs
namespace MailerMicroservice.Models;

public record MailRequest(string To, string Subject, string Body);
