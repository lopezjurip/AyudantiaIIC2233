- Aseg�rate de establecer al Chat2 - Launcher como "Proyecto Inicial" haciendo segundo click sobre �l.
	* M�s informaci�n ve la imagen adjunta.

- Hay un bug con el servidor cuando se cierra una instancia de este y luego se abre otra.
	* Al parecer deja inutilizado el IPEndPoint.
	* La soluci�n hasta el momento es cerrar todo el programa para volver a abrir otro servidor.

- Al parecer en C# los m�todo que involucran tareas con networking no tienen un Timeout como en otros lenguajes. 
	* Timeout es cuando la espera de una respuesta v�a internet demora mucho y se lanza una excepci�n.
	* Por esta raz�n varios catch(SocketException) nunca ser�n llamados.
	* Esos m�todos los hice pensando que exist�a un timeout, pero en .NET se espera para siempre hasta que llegue algo por el socket.

- Ojo con el Firewall, deben permitir que la aplicaci�n pueda utilizar acceso a internet. 
	* En caso de tener problemas se recomienda desactivar el firewall temporalmente.

Patricio L�pez.
pelopez2@uc.cl