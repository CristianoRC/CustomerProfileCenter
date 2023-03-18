using CustomerProfileCenter.Application.Customer;
using MongoDB.Bson;

namespace CustomerProfileCenter.Infra.Data.DatabaseObjects;

public class Customer
{
    public Customer()
    {
        Id = new ObjectId();
    }

    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public string DocumentHash { get; set; }
    public EDocumentType DocumentType { get; set; }
    public DateTime? Birthday { get; set; }
    public string CorporateName { get; set; }

    //TODO: Adicionar endereço e outros campos aqui!
}