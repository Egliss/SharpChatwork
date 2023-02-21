#!/bin/bash -xe

cd `dirname $0` && cd ../

FORMAT_SHARED_OPTIONS=" " #"--exclude *"

# Argument
USE_FIXLINT=$1
if [ ! "${USE_FIXLINT}" ]; then
  FORMAT_FIX_OPTIONS=" --verify-no-changes "
fi

dotnet tool install dotnet-format \
  --add-source https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet6/nuget/v3/index.json

dotnet tool run dotnet-format -- whitespace ${FORMAT_SHARED_OPTIONS} ${FORMAT_FIX_OPTIONS}
dotnet tool run dotnet-format -- style ${FORMAT_SHARED_OPTIONS} ${FORMAT_FIX_OPTIONS}

dotnet tool uninstall dotnet-format
