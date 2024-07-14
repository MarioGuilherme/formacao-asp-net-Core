namespace DevFreela.Application.ViewModels;

public class LoginUserViewModel {
    public string Email { get; private set; }
    public string Token { get; private set; }

    public LoginUserViewModel(string email, string token) {
        this.Email = email;
        this.Token = token;
    }
}