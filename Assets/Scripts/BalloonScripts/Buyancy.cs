/// <summary>
/// This class calculates the buoyancy force acting on an object in a fluid.
/// </summary>
public class Buoyancy
{

    public float objectVolume = 1.0f; //m^3
    /// <summary>
    /// Volume of the object in m^3
    /// </summary>
    public float ObjectVolume
    {
        get { return objectVolume; }
        set { objectVolume = value; }
    }
    
    public float fluidDensity = 1.204f; //kg/m^3 (air)
    /// <summary>
    /// Density of the fluid in kg/m^3
    /// </summary>
    public float FluidDensity
    {
        get { return fluidDensity; }
        set { fluidDensity = value; }
    }
    public float objectDensity = 1.0f; //kg/m^3
    /// <summary>
    /// Density of the object in kg/m^3
    /// </summary>
    public float ObjectDensity
    {
        get { return objectDensity; }
        set { objectDensity = value; }
    }
    /// <summary>
    /// Acceleration due to gravity in m/s^2
    /// </summary>
    public float accelerationDueToGravity = 9.81f; //m/s^2
    public float AccelerationDueToGravity
    {
        get { return accelerationDueToGravity; }
        set { accelerationDueToGravity = value; }
    }

    /// <summary>
    /// Buoyancy force acting on the object in Newtons
    /// </summary>
    public float BuoyancyForce{get; private set;}

    private void Calc()
    {
        //Fl = V (ρc - ρh ) ag (1)
        //where Fl = buoyancy force (N)
        //V = volume of the object (m3)
        //ρc = density of the fluid (kg/m3)
        //ρh = density of the object (kg/m3)
        //ag = acceleration due to gravity (9.81 m/s2)
        BuoyancyForce = objectVolume * (fluidDensity - objectDensity) * accelerationDueToGravity; 
    }
}
