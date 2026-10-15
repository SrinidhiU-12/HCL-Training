function BMI()
{
    let weight=document.getElementById("weight").value;
    let height=document.getElementById("height").value;
    height=height/100;
    let bmi=weight/(height*height)
    let category

    
    if(bmi<18.5)
    {
        category="underweight"

    }
    else if(bmi<25)
    {
        category="normalweight"

    }
    else if(bmi<30)
    {
        category="overweight"
   
    }
    else
    {
        category="obese"

    }


    document.getElementById("result").innerHTML="BMI is: "+bmi+"<br>Category: "+category;
}
func()