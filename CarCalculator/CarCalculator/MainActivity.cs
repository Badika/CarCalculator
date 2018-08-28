using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections;
using CarCalculator.Core;

namespace CarCalculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region fields
        EditText price;
        Spinner spinnerYear;
        Spinner spinnerEngineType;
        EditText engineVolume;

        TextView ED;
        TextView ID;
        TextView VAT;
        TextView fullPrice;

        private List<int> years;
        private List<string> engineTypes;

        Button clearButton;

        InputValues iv;
        OutputValues ov;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            FindResources();
            Initialization();
            AddEvents();
            
            ED = FindViewById<TextView>(Resource.Id.excise_duty);
            ID = FindViewById<TextView>(Resource.Id.import_duty);
            VAT = FindViewById<TextView>(Resource.Id.vat);
            fullPrice = FindViewById<TextView>(Resource.Id.full_price);
        }

        #region methods
        private void FindResources()
        {
            spinnerYear = FindViewById<Spinner>(Resource.Id.yearspin);
            spinnerEngineType = FindViewById<Spinner>(Resource.Id.engine_type);
            price = FindViewById<EditText>(Resource.Id.enter_price);
            engineVolume = FindViewById<EditText>(Resource.Id.enter_engine_volume);

            ED = FindViewById<TextView>(Resource.Id.excise_duty);
            ID = FindViewById<TextView>(Resource.Id.import_duty);
            VAT = FindViewById<TextView>(Resource.Id.vat);
            fullPrice = FindViewById<TextView>(Resource.Id.full_price);

            clearButton = FindViewById<Button>(Resource.Id.clear_btn);
        }
        private void Initialization()
        {
            YearsList yl = new YearsList(true);
            years = yl.Years;
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, years);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerYear.Adapter = adapter;

            EngineTypeList etl = new EngineTypeList();
            engineTypes = etl.EngineTypes;
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, engineTypes);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerEngineType.Adapter = adapter;

            iv = new InputValues();
        }
        private void AddEvents()
        {
            spinnerYear.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            spinnerEngineType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            price.TextChanged += TextChanged;
            engineVolume.TextChanged += TextChanged;

            clearButton.Click += (sender, e) =>
            {
                price.Text = string.Empty;
                spinnerYear.SetSelection(0);
                spinnerEngineType.SetSelection(0);
                engineVolume.Text = string.Empty;
            };
        }

        private void TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            SomeValuesChanged();
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            SomeValuesChanged();
        }

        private void SomeValuesChanged()
        {
            if (string.IsNullOrWhiteSpace(price.Text) || string.IsNullOrWhiteSpace(engineVolume.Text)) return;

            if (InputValues.IsPrice(price.Text) 
                && InputValues.IsEngineVolume(engineVolume.Text) 
                && InputValues.IsYear(years[spinnerYear.SelectedItemPosition]) 
                && InputValues.IsEngineType(engineTypes[spinnerEngineType.SelectedItemPosition].ToString())
                )

            {
                iv = new InputValues(Convert.ToInt32(price.Text), years[spinnerYear.SelectedItemPosition],
                    engineTypes[spinnerEngineType.SelectedItemPosition].ToString(), Convert.ToDouble(engineVolume.Text));
                ov = Calculating.PriceCalculating(iv);

                FillUpOutput(ov);
            }
            else return;
        }

        private void FillUpOutput(OutputValues outputValues)
        {
            ED.Text = outputValues.ExciseDuty.ToString();
            ID.Text = outputValues.ImportDuty.ToString();
            VAT.Text = outputValues.VAT.ToString();
            fullPrice.Text = outputValues.FullPrice.ToString();
        }
        #endregion
    }
}