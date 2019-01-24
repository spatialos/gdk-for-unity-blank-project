#!/usr/bin/env bash
set -e -u -x -o pipefail

cd "$(dirname "$0")/../"

ci/bootstrap.sh
.shared-ci/scripts/prepare-unity.sh
.shared-ci/scripts/prepare-unity-mobile.sh "$(pwd)/logs/PrepareUnityMobile.log"

source .shared-ci/scripts/pinned-tools.sh
source .shared-ci/scripts/profiling.sh

UNITY_PROJECT_DIR="$(pwd)/workers/unity"

markStartOfBlock "$0"

.shared-ci/scripts/build.sh "${UNITY_PROJECT_DIR}" UnityClient cloud mono "$(pwd)/logs/UnityClientBuild.log"
.shared-ci/scripts/build.sh "${UNITY_PROJECT_DIR}" UnityGameLogic cloud mono "$(pwd)/logs/UnityGameLogicBuild.log"
.shared-ci/scripts/build.sh "${UNITY_PROJECT_DIR}" AndroidClient local mono "$(pwd)/logs/AndroidClientBuild-mono.log"

if isMacOS; then
  .shared-ci/scripts/build.sh "${UNITY_PROJECT_DIR}" iOSClient local il2cpp "$(pwd)/logs/iOSClientBuild.log"
fi

markEndOfBlock "$0"
