clean:
	dotnet clean
build:
	cd GoodLinksParser && dotnet publish -c release
install:
	cp GoodLinksParser/bin/Release/net5.0/osx-x64/publish/GoodLinksParser ~/bin/goodlinksparser
