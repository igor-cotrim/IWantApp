using Flunt.Validations;

namespace src.Domain.Products;

public class Category : Entity
{
  public string Name { get; private set; }
  public bool Active { get; private set; }

  public Category(string name, string createdBy, string editedBy)
  {
    Name = name;
    Active = true;
    CreatedBy = createdBy;
    EditedBy = editedBy;
    CreatedOn = DateTime.Now;
    EditedOn = DateTime.Now;

    Validate();
  }

  public void Validate()
  {
    var contract = new Contract<Category>()
          .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório")
          .IsGreaterOrEqualsThan(Name, 3, "Name", "Nome tem que ser maior ou igual a 3")
          .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
          .IsNotNullOrEmpty(EditedBy, "EditedBy");

    AddNotifications(contract);
  }

  public void EditInfo(string name, bool active, string editedBy)
  {
    Active = active;
    Name = name;
    EditedBy = editedBy;
    EditedOn = DateTime.Now;

    Validate();
  }
}