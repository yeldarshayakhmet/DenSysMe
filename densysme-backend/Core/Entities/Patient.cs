using Core.Enums;

namespace Core.Entities;

public class Patient : Individual
{
    public string EmergencyPhoneNumber { get; set; } = string.Empty;
    public MaritalStatus? MaritalStatus { get; set; }
    public BloodType? BloodType { get; set; }
    public DateTime RegistrationDate { get; set; }
}