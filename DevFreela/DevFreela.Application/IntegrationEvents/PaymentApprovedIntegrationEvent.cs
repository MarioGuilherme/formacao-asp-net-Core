namespace DevFreela.Application.IntegrationEvents;

public class PaymentApprovedIntegrationEvent(int idProject) {
    public int IdProject { get; set; } = idProject;
}