using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NxMobSdk;

namespace involys
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public DeviceApi deviceApi;

        private ActionBar actionBar = null;
        private Inventory inventoryFragment = null;
        private SingleInventory singleInventoryFragment = null;
        private ReadTag readTagFragment = null;
        private ScanBarcode1D scanBarcode1dFragment = null;
        private ScanBarcode2D scanBarcode2dFragment = null;
        private WriteTag writeTagFragment = null;
        private Search searchFragment = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            deviceApi = new DeviceApi();

            deviceApi.Info();

            this.InitTab();
        }

        private void InitTab()
        {
            inventoryFragment = new Inventory();
            singleInventoryFragment = new SingleInventory();
            readTagFragment = new ReadTag();
            scanBarcode1dFragment = new ScanBarcode1D();
            scanBarcode2dFragment = new ScanBarcode2D();
            writeTagFragment = new WriteTag();
            searchFragment = new Search();

            actionBar = ActionBar;
            actionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            // Invetory tab
            ActionBar.Tab inventoryTab = ActionBar.NewTab();
            inventoryTab.SetTag(Resource.String.inventory);
            inventoryTab.SetText(Resource.String.inventory);
            inventoryTab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                Toast.MakeText(this, Resource.String.inventory, ToastLength.Short).Show();
                e.FragmentTransaction.Replace(Android.Resource.Id.Content, inventoryFragment);
            };

            // Single Invetory tab
            ActionBar.Tab singleInventoryTab = ActionBar.NewTab();
            singleInventoryTab.SetTag(Resource.String.single_inventory);
            singleInventoryTab.SetText(Resource.String.single_inventory);
            singleInventoryTab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                Toast.MakeText(this, Resource.String.single_inventory, ToastLength.Short).Show();
                e.FragmentTransaction.Replace(Android.Resource.Id.Content, singleInventoryFragment);
            };

            // Read Tag tab
            ActionBar.Tab readTagTab = ActionBar.NewTab();
            readTagTab.SetTag(Resource.String.read);
            readTagTab.SetText(Resource.String.read);
            readTagTab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                Toast.MakeText(this, Resource.String.read, ToastLength.Short).Show();
                e.FragmentTransaction.Replace(Android.Resource.Id.Content, readTagFragment);
            };

            // Write Tag tab
            ActionBar.Tab writeTagTab = ActionBar.NewTab();
            writeTagTab.SetTag(Resource.String.write);
            writeTagTab.SetText(Resource.String.write);
            writeTagTab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                Toast.MakeText(this, Resource.String.write, ToastLength.Short).Show();
                e.FragmentTransaction.Replace(Android.Resource.Id.Content, writeTagFragment);
            };

            // Search tab
            ActionBar.Tab searchTab = ActionBar.NewTab();
            searchTab.SetTag(Resource.String.search);
            searchTab.SetText(Resource.String.search);
            searchTab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                Toast.MakeText(this, Resource.String.search, ToastLength.Short).Show();
                e.FragmentTransaction.Replace(Android.Resource.Id.Content, searchFragment);
            };

            // Scan Barcode 1D tab
            ActionBar.Tab scanBarcode1dTab = ActionBar.NewTab();
            scanBarcode1dTab.SetTag(Resource.String.scan_1d);
            scanBarcode1dTab.SetText(Resource.String.scan_1d);
            scanBarcode1dTab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                Toast.MakeText(this, Resource.String.scan_1d, ToastLength.Short).Show();
                e.FragmentTransaction.Replace(Android.Resource.Id.Content, scanBarcode1dFragment);
            };

            // Scan Barcode 2D tab
            ActionBar.Tab scanBarcode2dTab = ActionBar.NewTab();
            scanBarcode2dTab.SetTag(Resource.String.scan_2d);
            scanBarcode2dTab.SetText(Resource.String.scan_2d);
            scanBarcode2dTab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                Toast.MakeText(this, Resource.String.scan_2d, ToastLength.Short).Show();
                e.FragmentTransaction.Replace(Android.Resource.Id.Content, scanBarcode2dFragment);
            };

            actionBar.AddTab(inventoryTab);
            actionBar.AddTab(singleInventoryTab);
            actionBar.AddTab(readTagTab);
            actionBar.AddTab(writeTagTab);
            actionBar.AddTab(searchTab);
            actionBar.AddTab(scanBarcode1dTab);
            actionBar.AddTab(scanBarcode2dTab);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnDestroy()
        {
            Clean();
            base.OnDestroy();
        }

        private void Clean()
        {
            deviceApi.Free();
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            Toast.MakeText(this, keyCode.ToString(), ToastLength.Short).Show();

            return base.OnKeyDown(keyCode, e);
        }


        public class MainCallback : NxMobSdk.IMainCallback
        {
            private readonly Context mContext;

            public MainCallback(Context _mContext)
            {
                mContext = _mContext;
            }

            public void OnComplete(string result)
            {
                Toast.MakeText(mContext, result, ToastLength.Short).Show();
            }
        }
    }
}