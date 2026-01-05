using Microsoft.Maui.Controls;

namespace LearningApp
{
    public partial class CartPage : ContentPage
    {
        private readonly AppDatabase _db;

        public CartPage(AppDatabase db)
        {
            InitializeComponent();
            _db = db;
            LoadCart();
        }

        private async void LoadCart()
        {
            var cartItems = await _db.GetCartItemsAsync(); // CartItem فقط CourseId داره

            // گرفتن اطلاعات کامل دوره‌ها
            var courses = await _db.GetCoursesAsync();
            var cartDisplay = cartItems.Select(c =>
                new
                {
                    CourseId = c.CourseId,
                    CourseTitle = courses.First(course => course.Id == c.CourseId).Title,
                    CourseImage = courses.First(course => course.Id == c.CourseId).Image
                }).ToList();

            CartCollection.ItemsSource = cartDisplay;
        }


        private async void Checkout_Clicked(object sender, EventArgs e)
        {
            var cartItems = await _db.GetCartItemsAsync();

            if (cartItems.Count == 0)
            {
                await DisplayAlert("سبد خرید خالی", "هیچ دوره‌ای برای خرید وجود ندارد", "باشه");
                return;
            }

            // اضافه کردن همه دوره‌ها به PurchasedCourse
            foreach (var item in cartItems)
            {
                await _db.SavePurchasedCourseAsync(item.CourseId);
            }

            // پاک کردن سبد خرید
            await _db.ClearCartAsync();

            await DisplayAlert("پرداخت موفق", "دوره‌ها فعال شدند", "باشه");

            // بازگشت به MyCoursesPage
            await Navigation.PushAsync(new MyCoursesPage(_db));
        }
    }
}
