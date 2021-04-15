using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace A889113.Actividad02
{
    class Program
    {
        static void Main(string[] args)
        {
            var Inventario = new List<Product>();
            Console.WriteLine("Hola. Este sistema lo ayudara a crear su catalogo de productos. Presione alguna tecla para continuar.");
            Console.ReadKey();
            int stopper = 0;
            int stopper2 = 0;
            while (stopper == 0)
            {
                Console.Clear();
                Console.WriteLine("Para comenzar, ingrese el codigo de producto que desea registrar en el sistema. Este debe ser un numero entero sin comas ni puntos: ");
                string strCodigoProducto = Console.ReadLine();
                while (Checker.Comprobador(strCodigoProducto) == false)
                {
                    Console.WriteLine("El codigo de producto debe ser un numero entero sin comas ni puntos, por favor intente de nuevo: ");
                    strCodigoProducto = Console.ReadLine();
                }
                int intCodigoProducto = int.Parse(strCodigoProducto);
                bool containsItem = Inventario.Any(item => item.Codigo == intCodigoProducto);
                while (containsItem == true)
                {
                    Console.WriteLine("Ese codigo de producto ya esta en la lista. Intente nuevamente");
                    strCodigoProducto = Console.ReadLine();
                    while (Checker.Comprobador(strCodigoProducto) == false)
                    {
                        Console.WriteLine("El codigo de producto debe ser un numero entero sin comas ni puntos, por favor intente de nuevo: ");
                        strCodigoProducto = Console.ReadLine();
                    }
                    intCodigoProducto = int.Parse(strCodigoProducto);
                    containsItem = Inventario.Any(item => item.Codigo == intCodigoProducto);
                }

                Console.WriteLine("Ahora ingrese el stock del producto. Este debe ser un numero entero sin comas ni puntos: ");
                string strStockProducto = Console.ReadLine();
                while (Checker.Comprobador(strStockProducto) == false)
                {
                    Console.WriteLine("El stock del producto debe ser un numero entero sin comas ni puntos, por favor intente de nuevo: ");

                    strStockProducto = Console.ReadLine();
                }
                int intStockProducto = int.Parse(strStockProducto);

                var producto = new Product
                {
                    Codigo = intCodigoProducto,
                    Stock = intStockProducto,
                };

                Inventario.Add(producto);
                Console.WriteLine("Ha cargado el producto con exito! Presione cualquier boton para continuar ");
                Console.ReadKey();
                stopper2 = 0;
                while (stopper2 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Por favor elija una opcion del siguiente menu escribiendo el numero correspondiente:");
                    Console.WriteLine("1 - Si desea registrar otro producto.");
                    Console.WriteLine("2 - Si desea realizar un pedido");
                    Console.WriteLine("3 - Si desea realizar una entrega");
                    Console.WriteLine("4 - Si desea sallir de la aplicacion");

                    string respuesta = Console.ReadLine();

                    while ((respuesta != "1") && (respuesta != "2") && (respuesta != "3") && (respuesta != "4"))
                    {
                        Console.WriteLine("Por favor escriba 'registrar', 'salir'. Intente nuevamente");
                        respuesta = Console.ReadLine();
                    }

                    switch (respuesta)
                    {
                        case ("1"):
                            stopper = 0;
                            stopper2 = 1;
                            break;

                        case ("2"):
                            Console.Clear();
                            Console.WriteLine("Estos son los productos disponibles en el inventario:");
                            foreach (Product product in Inventario)
                            {
                                Console.WriteLine(product.ToString());
                            }

                            Console.WriteLine("Por favor escribe el codigo de producto al que quieres realizar un pedido.");

                            string codigobuscadopedido = Console.ReadLine();
                            int intCodigoBuscadoPedido;

                            bool isParsable = int.TryParse(codigobuscadopedido, out intCodigoBuscadoPedido);
                            while (isParsable == false)
                            {
                                Console.WriteLine("Debe ser un numero. Intente de nuevo");
                                codigobuscadopedido = Console.ReadLine();
                                isParsable = int.TryParse(codigobuscadopedido, out intCodigoBuscadoPedido);
                            }

                            int indexPedido = Inventario.FindIndex(item => item.Codigo == intCodigoBuscadoPedido);
                            while (indexPedido < 0)
                            {
                                Console.WriteLine("El codigo que buscas no existe. Por favor intenta de nuevo");
                                codigobuscadopedido = Console.ReadLine();

                                isParsable = int.TryParse(codigobuscadopedido, out intCodigoBuscadoPedido);
                                while (isParsable == false)
                                {
                                    Console.WriteLine("Debe ser un numero. Intente de nuevo");
                                    codigobuscadopedido = Console.ReadLine();
                                    isParsable = int.TryParse(codigobuscadopedido, out intCodigoBuscadoPedido);
                                }

                                indexPedido = Inventario.FindIndex(item => item.Codigo == intCodigoBuscadoPedido);
                            }
                            Console.WriteLine("Encontramos el producto! Cuanto desea pedir?");
                            string pedido = Console.ReadLine();
                            int intPedido;
                            bool isParsable2 = int.TryParse(pedido, out intPedido);
                            while ((isParsable2 == false) || (intPedido < 0))
                            {
                                Console.WriteLine("Debe ser un numero entero mayor a 0. Intente de nuevo");
                                pedido = Console.ReadLine();
                                isParsable2 = int.TryParse(pedido, out intPedido);
                            }
                            intPedido = int.Parse(pedido);

                            while (Inventario[indexPedido].Stock - intPedido < 0)
                            {
                                Console.WriteLine("El stock no puede ser menor a 0. Ingrese otra cantidad:");
                                pedido = Console.ReadLine();

                                isParsable2 = int.TryParse(pedido, out intPedido);
                                while (isParsable2 == false)
                                {
                                    Console.WriteLine("Debe ser un numero. Intente de nuevo");
                                    pedido = Console.ReadLine();
                                    isParsable2 = int.TryParse(pedido, out intPedido);
                                }
                                intPedido = int.Parse(pedido);
                            }
                            Inventario[indexPedido].Stock = Inventario[indexPedido].Stock - intPedido;

                            Console.WriteLine("Pedido realizado! El nuevo inventario es el siguiente:");
                            foreach (Product product in Inventario)
                            {
                                Console.WriteLine(product.ToString());
                            }
                            Console.ReadKey();
                            break;

                        case ("3"):
                            Console.Clear();
                            Console.WriteLine("Estos son los productos disponibles en inventario:");
                            foreach (Product product in Inventario)
                            {
                                Console.WriteLine(product.ToString());
                            }

                            Console.WriteLine("Por favor escribe el codigo de producto al que quieres realizar una entrega");

                            string codigobuscadoentrega = Console.ReadLine();
                            int intCodigoBuscadoEntrega;
                            bool isParsableEntrega = int.TryParse(codigobuscadoentrega, out intCodigoBuscadoEntrega);

                            while (isParsableEntrega == false)
                            {
                                Console.WriteLine("Debe ser un numero. Intente de nuevo");
                                codigobuscadoentrega = Console.ReadLine();
                                isParsableEntrega = int.TryParse(codigobuscadoentrega, out intCodigoBuscadoEntrega);
                            }
                            int indexEntrega = Inventario.FindIndex(item => item.Codigo == intCodigoBuscadoEntrega);

                            while (indexEntrega < 0)
                            {
                                Console.WriteLine("Ese codigo de producto no existe. Intenta de nuevo");
                                codigobuscadoentrega = Console.ReadLine();
                                isParsableEntrega = int.TryParse(codigobuscadoentrega, out intCodigoBuscadoEntrega);

                                while (isParsableEntrega == false)
                                {
                                    Console.WriteLine("Debe ser un numero. Intente de nuevo");
                                    codigobuscadoentrega = Console.ReadLine();
                                    isParsableEntrega = int.TryParse(codigobuscadoentrega, out intCodigoBuscadoEntrega);
                                }
                                indexEntrega = Inventario.FindIndex(item => item.Codigo == intCodigoBuscadoEntrega);
                            }

                            Console.WriteLine("Encontramos el producto! Cuanto stock desea entregar?");
                            string entrega = Console.ReadLine();
                            int intEntrega;
                            bool isParsable3 = int.TryParse(entrega, out intEntrega);
                            while ((isParsable3 == false) || (intEntrega < 0))
                            {
                                Console.WriteLine("Debe ser un numero entero mayor a 0. Intente de nuevo");
                                entrega = Console.ReadLine();
                                isParsable3 = int.TryParse(entrega, out intEntrega);
                            }
                            intEntrega = int.Parse(entrega);

                            Inventario[indexEntrega].Stock = Inventario[indexEntrega].Stock + intEntrega;

                            Console.WriteLine("Pedido realizado! El nuevo inventario es el siguiente:");
                            foreach (Product product in Inventario)
                            {
                                Console.WriteLine(product.ToString());
                            }
                            Console.ReadKey();
                            break;

                        case ("4"):
                            stopper = 1;
                            stopper2 = 1;
                            break;
                    }
                }

            }

        }

        class Product
        {
            public int Codigo { get; set; }
            public int Stock { get; set; }

            public override string ToString()
            {
                return string.Format("Codigo de Producto: {0}, Stock del producto: {1}", this.Codigo, this.Stock);
            }
        }

        public static class Checker
        {
            public static bool Comprobador(string valor)
            {
                int num;
                if (valor == null)
                {
                    return false;
                }
                else
                {
                    bool conversion = int.TryParse(valor, out num);
                    if (conversion == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }


            }
        }

    }
}
