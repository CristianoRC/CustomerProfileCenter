using CustomerProfileCenter.Application.Customer;

namespace CustomerProfileCenter.Infra.Data.DatabaseObjects;

public class Customer
{
    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public string DocumentHash { get; set; }
    public EDocumentType DocumentType { get; set; }
    public DateTime? Birthday { get; set; }
    public string CorporateName { get; set; }

    //TODO: Adicionar endereço e outros campos aqui!
}