using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections;

namespace CarCalculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText price;
        Spinner spinnerYear;
        Spinner spinnerEngineType;
        EditText engineVolume;

        TextView ED;
        TextView ID;
        TextView VAT;
        TextView fullPrice;
        private List<int> years;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            spinnerYear = FindViewById<Spinner>(Resource.Id.yearspin);

            spinnerYear.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            AddYearSpinner(spinnerYear);

            spinnerEngineType = FindViewById<Spinner>(Resource.Id.engine_type);

            spinnerEngineType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            AddSpinner(spinnerEngineType);



            price = FindViewById<EditText>(Resource.Id.enter_price);
            price.AfterTextChanged += AfterTextChanged;

            engineVolume = FindViewById<EditText>(Resource.Id.enter_engine_volume);
            engineVolume.AfterTextChanged += AfterTextChanged;

            ED = FindViewById<TextView>(Resource.Id.excise_duty);
            ID = FindViewById<TextView>(Resource.Id.import_duty);
            VAT = FindViewById<TextView>(Resource.Id.vat);
            fullPrice = FindViewById<TextView>(Resource.Id.full_price);


            Button clearButton = FindViewById<Button>(Resource.Id.clear_btn);
            clearButton.Click += (sender, e) =>
            {
                price.Text = string.Empty;
                spinnerYear.SetSelection(0);
                spinnerEngineType.SetSelection(0);
                engineVolume.Text = string.Empty;
            };
        }

        private void AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            SomeValuesChanged();
        }

        private void SomeValuesChanged()
        {
            if (string.IsNullOrWhiteSpace(price.Text) || string.IsNullOrWhiteSpace(engineVolume.Text)) return;

            if (IsPrice() && IsEngineVolume() && IsYear() && IsEngineType())
                Calculate();

            else return;
        }

        private void Calculate()
        {
            double exciseDuty;
            double importDuty;
            double vat;
            double _fullPrice;

            double engineTypeCoef;

            switch((spinnerEngineType.SelectedItemPosition).ToString())
            {
                case "0":
                    engineTypeCoef = 50;
                    break;
                case "1":
                    engineTypeCoef = 75;
                    break;
                default:
                    engineTypeCoef = 50;
                    break;
            }

            var v = Convert.ToDouble(engineVolume.Text);
            var fullYears = DateTime.Now.Year - years[spinnerYear.SelectedItemPosition];

            if (fullYears <= 0)
                fullYears = 1;


            exciseDuty = engineTypeCoef * v * fullYears;

            System.Diagnostics.Debug.WriteLine("year spinner pos = " + spinnerEngineType.SelectedItemPosition.ToString());

            System.Diagnostics.Debug.WriteLine(string.Format("V = {0}, Coef = {1}, Full years = {2}, Excise Duty = {3}", v, engineTypeCoef, fullYears, exciseDuty));

            importDuty = (Convert.ToDouble(price.Text) + exciseDuty) * 0.1;
            vat = importDuty * 2;
            _fullPrice = Convert.ToDouble(price.Text) + vat + exciseDuty + importDuty;


            ED.Text = exciseDuty.ToString();
            ID.Text = importDuty.ToString();
            VAT.Text = vat.ToString();
            fullPrice.Text = _fullPrice.ToString();
        }

        private bool IsEngineType()
        {
            if (spinnerEngineType.SelectedItemPosition >= 0)
                return true;
            return false;
        }

        private bool IsYear()
        {
            if (spinnerYear.SelectedItemPosition >= 0)
                return true;
            return false;
        }

        private bool IsEngineVolume()
        {
            double ev;
            try
            {
                ev = Convert.ToDouble(engineVolume.Text);
            }
            catch (Exception) { return false; }
            return true;
        }

        private bool IsPrice()
        {
            int p;
            try
            {
                p = Convert.ToInt32(price.Text);
            }
            catch (Exception) { return false; }
            return true;
        }


        private void AddSpinner(Spinner s)
        {
            
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.engine_list, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            s.Adapter = adapter;
        }

        private void AddYearSpinner(Spinner s)
        {
            years = new List<int>();
            int thisYear = DateTime.Now.Year;
            for (int i = thisYear; i >= 1900; i--)
            {
                years.Add(i);
            }
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, years);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            s.Adapter = adapter;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Spinner spinner = (Spinner)sender;
            //string toast = string.Format("Selected engine type is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
            SomeValuesChanged();
        }

        private void spinnerYear_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Spinner spinner = (Spinner)sender;
            //string toast = string.Format("Selected year is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

    }
}