using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectPattern.ContactsData
{
    public class ContactDataBuilder
    {
        private readonly ContactData _data = new ContactData();

        public ContactDataBuilder WithEmail(string email)
        {
            _data.Email = email;
            return this;
        }

        public ContactDataBuilder WithPhone(string phone)
        {
            _data.Phones.Add(phone);
            return this;
        }

        public ContactDataBuilder WithPhones(IEnumerable<string> phones)
        {
            _data.Phones.AddRange(phones);
            return this;
        }

        public ContactDataBuilder Withsocial(string social)
        {
            _data.Socials.Add(social);
            return this;
        }

        public ContactDataBuilder WithSocials(IEnumerable<string> socials)
        {
            _data.Socials.AddRange(socials);
            return this;
        }

        public ContactData Build()
        {
            return _data;
        }
    }
}
