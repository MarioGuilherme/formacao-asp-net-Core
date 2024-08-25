namespace DevFreela.Application.ViewModels;

public class UserViewModel(string fullName, string email) {
    public string FullName { get; private set; } = fullName;
    public string Email { get; private set; } = email;
}