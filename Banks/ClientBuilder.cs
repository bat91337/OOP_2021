namespace Banks
{
    public class ClientBuilder
    {
        private string _nameClient;
        private string _address;
        private string _passport;
        private string _firstNameClient;
        private string _numberPhone;
        public ClientBuilder SetFirstNAme(string firstNAme)
        {
            _firstNameClient = firstNAme;
            return this;
        }

        public ClientBuilder SetName(string name)
        {
            _nameClient = name;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            _passport = passport;
            return this;
        }

        public ClientBuilder SetNumberPhone(string numberPhone)
        {
            _numberPhone = numberPhone;
            return this;
        }

        public Client Build()
        {
            var newClient = new Client(_firstNameClient, _nameClient, _passport, _address, _numberPhone);
            return newClient;
        }
    }
}