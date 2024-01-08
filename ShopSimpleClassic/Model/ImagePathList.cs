namespace ShopSimpleClassic.Model
{
    public class ImagePathList
    {
        private string _id; // mã sản phẩm
        private string _path;
        private string _image;

        public string Id { get => _id; set => _id = value; }
        public string Path { get => _path; set => _path = value; }
        public string Image { get => _image; set => _image = value; }
    }
}