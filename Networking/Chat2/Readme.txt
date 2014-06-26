- Asegúrate de establecer al Chat2 - Launcher como "Proyecto Inicial" haciendo segundo click sobre él.
	* Más información ve la imagen adjunta.

- Hay un bug con el servidor cuando se cierra una instancia de este y luego se abre otra.
	* Al parecer deja inutilizado el IPEndPoint.
	* La solución hasta el momento es cerrar todo el programa para volver a abrir otro servidor.

- Al parecer en C# los método que involucran tareas con networking no tienen un Timeout como en otros lenguajes. 
	* Timeout es cuando la espera de una respuesta vía internet demora mucho y se lanza una excepción.
	* Por esta razón varios catch(SocketException) nunca serán llamados.
	* Esos métodos los hice pensando que existía un timeout, pero en .NET se espera para siempre hasta que llegue algo por el socket.

- Ojo con el Firewall, deben permitir que la aplicación pueda utilizar acceso a internet. 
	* En caso de tener problemas se recomienda desactivar el firewall temporalmente.

Patricio López.
pelopez2@uc.cl