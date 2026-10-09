/*async function func()
{
    let response = await fetch("Info.txt");
    let data = await response.text();
    document.getElementById("demo").innerHTML = data;
}
func();*/
async function func()
{
    let response = await fetch("Info.txt");
    let data = await response.text();
    display(data)
}
display=(data)=>
{
    document.getElementById("demo").innerHTML = data;
}
func();