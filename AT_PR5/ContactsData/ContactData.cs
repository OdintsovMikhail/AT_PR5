using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AT_PR5.ContactsData
{
    public class ContactData
    {
        public string Email { get; set; }
        public List<string> Phones { get; set; } = new List<string>();
        public List<string> Socials { get; set; } = new List<string>();

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not ContactData)
            {
                return false;
            }
            else
            {
                ContactData other = (ContactData)obj;

                return Email == other.Email &&
                    Phones.SequenceEqual(other.Phones) &&
                    Socials.SequenceEqual(other.Socials);
                }
        }

        public override int GetHashCode()
        {
            int hashEmail = Email?.GetHashCode() ?? 0;
            int hashPhones = Phones != null ? Phones.Aggregate(0, (hash, phone) => hash ^ phone.GetHashCode()) : 0;
            int hashSocials = Socials != null ? Socials.Aggregate(0, (hash, social) => hash ^ social.GetHashCode()) : 0;

            return hashEmail ^ hashPhones ^ hashSocials;
        }
    }
}
