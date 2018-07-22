# Password Store

WPF Tray bar application for storing and retrieving credentials.

Password is copied to clipboard after you select context from popup window.

Application stores credentials with passwords encrypted.

Supported file formats:


.txt


text file must follow this pattern:


CONTEXT LOGIN PASSWORD


.xml


<Credential>
	<Name>CONTEXT</Name>
	<Login>LOGIN</Login>
	<Password>PASSWORD</Password>
</Credential>


### Installing

Build

Release\Setup.msi


### Prerequisites

.NET Framework 4.6.1

## Authors

* **Józef Podlecki** - *Initial work* - [Jozefpodlecki](https://github.com/Jozefpodlecki)
