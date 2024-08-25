namespace DevFreela.Core.IntegrationEvents;

public class PaymentApprovedIntegrationEvent(int idProject) {
    public int IdProject { get; set; } = idProject;
}