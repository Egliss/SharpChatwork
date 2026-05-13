#!/bin/bash -xe

cd `dirname $0` && cd ../

COVERAGE_FILE="coverage.cobertura.xml"
REPORT_DIR="coverage-report"

dotnet tool restore
dotnet test --coverage --coverage-output-format cobertura --coverage-output ${COVERAGE_FILE}
dotnet reportgenerator "-reports:**/TestResults/${COVERAGE_FILE}" "-targetdir:${REPORT_DIR}" "-reporttypes:Html;Badges;TextSummary"

cat ${REPORT_DIR}/Summary.txt
