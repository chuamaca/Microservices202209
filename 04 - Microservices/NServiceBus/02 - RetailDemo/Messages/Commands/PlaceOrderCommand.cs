using NServiceBus;

namespace Messages.Commands
{
    //Un mensaje es una colección de datos enviados a través de la comunicación unidireccional entre dos extremos. 
    //En NServiceBus, definimos el mensaje a través de clases simples.

    //Los mensajes son contratos de datos y, como tales, se comparten entre varios puntos de conexión. Por lo tanto, 
    //no debe colocar las clases en el mismo ensamblado con los extremos; deben vivir en una biblioteca de clases separada.
    //Los ensamblados de mensajes deben ser completamente autónomos, lo que significa que deben contener solo los tipos de mensaje NServiceBus 
    //y los tipos auxiliares requeridos por los propios mensajes.Por ejemplo, si un mensaje usa un tipo de enumeración para una de sus propiedades, 
    //ese tipo de enumeración también debe estar contenido en el mismo ensamblado de mensaje.
    public class PlaceOrderCommand:ICommand
    {
        public string OrderId { get; set; }
    }
}