using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterWindow : MonoBehaviour
{
    Text paramName;
    Slider minSlider;
    Slider maxSlider;
    Slider rateSlider;
    Slider offsetSlider;
    Slider randomSlider;

    InputField minInput;
    InputField maxInput;
    InputField rateInput;
    InputField offsetInput;
    InputField randomInput;

    Text minLabel;
    Text maxLabel;
    Text rateLabel;
    Text offsetLabel;
    Text randomLabel;

    Toggle cyclicalToggle;
    Toggle startAscendingToggle;
    Toggle ascendToggle;

    FloatParameter currentParameter;

    readonly Color invalidInputColor = new Color(1f, 0.5f, 0.5f);

    private void Start()
    {
        StartInit();
    }
    public void StartInit()
    {
        paramName = transform.Find("DragPanel").Find("ParameterName").GetComponent<Text>();

        minSlider = transform.Find("MinParam").Find("Slider").GetComponent<Slider>();
        maxSlider = transform.Find("MaxParam").Find("Slider").GetComponent<Slider>();
        rateSlider = transform.Find("RateParam").Find("Slider").GetComponent<Slider>();
        offsetSlider = transform.Find("OffsetParam").Find("Slider").GetComponent<Slider>();
        randomSlider = transform.Find("RandomnessParam").Find("Slider").GetComponent<Slider>();

        minSlider.onValueChanged.AddListener(delegate { MinSliderChanged(); });
        maxSlider.onValueChanged.AddListener(delegate { MaxSliderChanged(); });
        rateSlider.onValueChanged.AddListener(delegate { RateSliderChanged(); });
        offsetSlider.onValueChanged.AddListener(delegate { OffsetSliderChanged(); });
        randomSlider.onValueChanged.AddListener(delegate { RateSliderChanged(); });

        minInput = transform.Find("MinParam").Find("InputField").GetComponent<InputField>();
        maxInput = transform.Find("MaxParam").Find("InputField").GetComponent<InputField>();
        rateInput = transform.Find("RateParam").Find("InputField").GetComponent<InputField>();
        offsetInput = transform.Find("OffsetParam").Find("InputField").GetComponent<InputField>();
        randomInput = transform.Find("RandomnessParam").Find("InputField").GetComponent<InputField>();

        minInput.onEndEdit.AddListener(delegate { MinInputChanged(); });
        maxInput.onEndEdit.AddListener(delegate { MaxInputChanged(); });
        rateInput.onEndEdit.AddListener(delegate { RateInputChanged(); });
        offsetInput.onEndEdit.AddListener(delegate { OffsetInputChanged(); });
        randomInput.onEndEdit.AddListener(delegate { RandomInputChanged(); });

        minLabel = transform.Find("MinParam").Find("Label").GetComponent<Text>();
        maxLabel = transform.Find("MaxParam").Find("Label").GetComponent<Text>();
        rateLabel = transform.Find("RateParam").Find("Label").GetComponent<Text>();
        offsetLabel = transform.Find("OffsetParam").Find("Label").GetComponent<Text>();
        randomLabel = transform.Find("RandomnessParam").Find("Label").GetComponent<Text>();


        cyclicalToggle = transform.Find("Cyclical").GetComponent<Toggle>();
        startAscendingToggle = transform.Find("StartAscending").GetComponent<Toggle>();
        ascendToggle = transform.Find("Ascend").GetComponent<Toggle>();

       
    }
    public void InitWithParameter(FloatParameter f)
    {
        currentParameter = f;

        if (paramName == null)
            StartInit();

        minSlider.minValue = currentParameter.ClampRange.min;
        minSlider.maxValue = currentParameter.ClampRange.max;
        maxSlider.minValue = currentParameter.ClampRange.min;
        maxSlider.maxValue = currentParameter.ClampRange.max;
        rateSlider.minValue = currentParameter.ClampRange.min;
        rateSlider.maxValue = currentParameter.ClampRange.max;
        offsetSlider.minValue = currentParameter.ClampRange.min;
        offsetSlider.maxValue = currentParameter.ClampRange.max;
        randomSlider.minValue = currentParameter.ClampRange.min;
        randomSlider.maxValue = currentParameter.ClampRange.max;

        paramName.text = f.Name;
        minSlider.value = currentParameter.Min;
        maxSlider.value = currentParameter.Max;
        rateSlider.value = currentParameter.Rate;
        offsetSlider.value = currentParameter.Offset;
        randomSlider.value = currentParameter.Randomness;
        cyclicalToggle.isOn = currentParameter.IsCyclical;
        ascendToggle.isOn = currentParameter.Ascend;
        startAscendingToggle.isOn = currentParameter.StartAscending;

        

        UpdateSliders();
    }

    void MinSliderChanged()
    {
        if (maxSlider.value < minSlider.value)
            maxSlider.value = minSlider.value;

        UpdateSliders();
        currentParameter.SetMin(minSlider.value);
        minInput.text = minSlider.value.ToString();
    }

    void MaxSliderChanged()
    {

        if (minSlider.value > maxSlider.value)
            minSlider.value = maxSlider.value;
        UpdateSliders();
        currentParameter.SetMax(maxSlider.value);

    }

    void RateSliderChanged()
    {
        UpdateSliders();
        currentParameter.SetRate(rateSlider.value);
    }

    void OffsetSliderChanged()
    {
        UpdateSliders();
        currentParameter.SetOffset(offsetSlider.value);
    }

    void RandomSliderChanged()
    {
        UpdateSliders();
        currentParameter.SetRandom(randomSlider.value);
    }

    void MinInputChanged()
    {
        float inputVal = 0;

        bool validInput = false;

        if (float.TryParse(minInput.text, out inputVal))
        {
            if (inputVal > minSlider.minValue &&inputVal < minSlider.maxValue )
            {
                minSlider.value = inputVal;
                validInput = true;
            }
        }

        if (validInput)
            MinSliderChanged();
        else
            minInput.text = minSlider.value.ToString();
    }

    void MaxInputChanged()
    {
        float inputVal = 0;

        bool validInput = false;

        if (float.TryParse(maxInput.text, out inputVal))
        {
            if (inputVal > maxSlider.minValue && inputVal < maxSlider.maxValue)
            {
                maxSlider.value = inputVal;
                validInput = true;
            }
        }

        if (validInput)
            MaxSliderChanged();
        else
            maxInput.text = maxSlider.value.ToString();
    }

    void RateInputChanged()
    {
        float inputVal = 0;
        bool validInput = false;

        if(float.TryParse(rateInput.text , out inputVal))
        {
            if(inputVal > rateSlider.minValue && inputVal < rateSlider.maxValue)
            {
                rateSlider.value = inputVal;
                validInput = true;
            }
        }

        if (validInput)
            RateSliderChanged();
        else
            rateInput.text = rateSlider.value.ToString();
    }

    void OffsetInputChanged()
    {
        float inputVal = 0;
        bool validInput = false;

        if (float.TryParse(offsetInput.text, out inputVal))
        {
            if (inputVal > offsetSlider.minValue && inputVal < offsetSlider.maxValue)
            {
                offsetSlider.value = inputVal;
                validInput = true;
            }
        }

        if (validInput)
            OffsetSliderChanged();
        else
            offsetInput.text = offsetSlider.value.ToString();
    }

    void RandomInputChanged()
    {
        float inputVal = 0;
        bool validInput = false;

        if (float.TryParse(randomInput.text, out inputVal))
        {
            if (inputVal > randomSlider.minValue && inputVal < randomSlider.maxValue)
            {
                randomSlider.value = inputVal;
                validInput = true;
            }
        }

        if (validInput)
            RandomSliderChanged();
        else
            randomInput.text = randomSlider.value.ToString();
    }
   

    void UpdateSliders()
    {
        rateSlider.maxValue = maxSlider.value;
        offsetSlider.minValue = minSlider.value;
        offsetSlider.maxValue = maxSlider.value;
        randomSlider.maxValue = maxSlider.value;


        minSlider.value = Mathf.Clamp(minSlider.value, minSlider.value, maxSlider.value);
        minSlider.value = Mathf.Clamp(minSlider.value, minSlider.value, maxSlider.value);
        rateSlider.value = Mathf.Clamp(rateSlider.value, 0, maxSlider.value);
        offsetSlider.value = Mathf.Clamp(offsetSlider.value, minSlider.value, maxSlider.value);
        randomSlider.value = Mathf.Clamp(randomSlider.value, 0, maxSlider.value);
        

        //update the labels to show the range of the sliers to 3 digits
        string min = minSlider.value.ToString().Substring(0, (int)Mathf.Min(3, minSlider.value.ToString().Length));
        string max = maxSlider.value.ToString().Substring(0, (int)Mathf.Min(3, maxSlider.value.ToString().Length));
        string newRange = "{ " + min + ", " + max  + " }";

        minLabel.text = "Min {" + minSlider.minValue.ToString().Substring(0,(int)Mathf.Min(3,minSlider.minValue.ToString().Length)) + ", " + max + "}";
        maxLabel.text = "Max {" + min + ", " + maxSlider.maxValue.ToString().Substring(0,(int)Mathf.Min(3,maxSlider.maxValue.ToString().Length)) + "}";

        rateLabel.text = "Rate " + newRange;
        offsetLabel.text = "Offset " + newRange;
        randomLabel.text = "Rand " + newRange;

        minInput.text = minSlider.value.ToString();
        maxInput.text = maxSlider.value.ToString();
        rateInput.text = rateSlider.value.ToString();
        offsetInput.text = offsetSlider.value.ToString();
        randomInput.text = randomSlider.value.ToString();

    }

    public FloatParameter CurrentParameter
    {
        get { return currentParameter; }
    }

}
