namespace _3._3;

public class Program
{
    static void Main( string[] args )
    {
        Random rnd = new Random();

        for ( int i = 0; i < 10; i++ )
        {
            Console.WriteLine( rnd.Next( 0, 10 ) );
        }
    }
}
