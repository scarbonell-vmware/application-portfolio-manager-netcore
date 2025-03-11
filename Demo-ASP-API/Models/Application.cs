namespace Demo_ASP_API.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Organization { get; set; }
        public string? BusinessEntity { get; set; }
        public string? BusinessOwner { get; set; }
        public string? Identifier { get; set; }

        public void setName (string name)
        {
            this.Name=name;
        }

        public void setIdentifier(string identifier)
        {
            this.Identifier=identifier;
        }

        public void setDescription(string description)
        {
            this.Description = description;
        }

        public void setOrganization(string organization)
        {
            this.Organization = organization;
        }

        public void setBusinessEntity(string businessEntity)
        {
            this.BusinessEntity = businessEntity;
        }

        public void setBusinessOwner(string businessOwner)
        {
            this.BusinessOwner=businessOwner;
        }

    }
}
