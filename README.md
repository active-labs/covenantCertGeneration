# covenantCertGeneration
A small program to create a self signed certificate for use with the Covenant C2 framework (https://github.com/cobbr/Covenant/)

## Usage
Clone the repository:
```
git clone https://github.com/active-labs/covenantCertGeneration.git
```

Build the project with dotnet `v4.7.2` or larger and run the binary to generate the certificates.

```
PS C:\source\repos\covenantCertGeneration\covenantCertGeneration\bin\Release> .\covenantCertGeneration.exe
Creating cert...
PS C:\source\repos\covenantCertGeneration\covenantCertGeneration\bin\Release> dir


    Directory: C:\source\repos\covenantCertGeneration\covenantCertGeneration\bin\Release


Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
-a----         5/24/2021   1:13 PM           2414 covenant-dev-private.pfx
-a----         5/24/2021   1:13 PM            738 covenant-dev-public.cer
-a----         5/12/2021   3:53 PM           6656 covenantCertGeneration.exe
```
