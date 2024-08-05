using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFriends.Models
{
    public class Friend
    {
        public Friend()
        {
            images = new List<Image>();
        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "שם משפחה ")]
        public string lastName { get; set; }

        [Display(Name = " שם מלא"), NotMapped]
        public string FullName { get { return FirstName + " " + lastName; } }

        [Display(Name = "מספר טלפון "), Phone(ErrorMessage = "Phone number not valid")]
        public string phoneNumber { get; set; }

        [Display(Name = "כתובת מייל "), EmailAddress(ErrorMessage = "email address not valid")]
        public string emailAddress { get; set; }

        public List<Image> images { get; set; }

        [Display(Name = "הוספת תמונה" ),NotMapped]
        public IFormFile SetImage
        {
            get { return null; }
            set
            {
                if (value == null)
                {
                    return;
                }

                // יצירת מקום בזיכרון עבור קובץ
                MemoryStream stream = new MemoryStream();

                value.CopyTo(stream);

                // המרת המקום בזיכרון שיצרנו לבייטים
                byte[] streamArray = stream.ToArray();

                // הוספת הת מונה לרשימת התמונות של החבר
                AddImage(streamArray);
            }
        }

        public void AddImage(byte[] image)
        {
            Image img = new()
            {
                MyImage = image,
                friend = this
            };
           images.Add(img);
        }
    }
}
