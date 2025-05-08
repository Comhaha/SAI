pipeline {
    agent any

    options {
        disableConcurrentBuilds(abortPrevious: true)
        buildDiscarder(logRotator(numToKeepStr: '10'))
    }

    stages {
        stage('변경 디렉터리 탐색') {
            steps {
                script {
                    def since = env.GIT_PREVIOUS_SUCCESSFUL_COMMIT ?: 'HEAD~1'
                    def diff  = sh(
                        script: "git diff --name-only ${since} ${env.GIT_COMMIT}",
                        returnStdout: true
                    ).trim().split('\n')

                    changedTopDirs = diff.findAll { it.contains('/') }
                                         .collect  { it.tokenize('/')[0] }
                                         .unique()

                    echo "변경된 최상위 폴더: ${changedTopDirs}"
                }
            }
        }

        stage('Child Jobs Fan-out') {
            steps {
                script {
                    def fanout = [:]

                    if (changedTopDirs.contains('frontend')) {
                        fanout['frontend'] = {
                            build job: 'frontend',
                                  wait: false,
                                  propagate: false,
                                  parameters: [
                                      string(name: 'GIT_SHA', value: env.GIT_COMMIT.take(7))
                                  ]
                        }
                    }

                    if (changedTopDirs.contains('backend')) {
                        fanout['backend'] = {
                            build job: 'backend',
                                  wait: false,
                                  propagate: false,
                                  parameters: [
                                      string(name: 'GIT_SHA', value: env.GIT_COMMIT.take(7))
                                  ]
                        }
                    }

                    // if (changedTopDirs.contains('c#')) {
                    //     fanout['c#'] = {
                    //         build job: 'csharp',
                    //               wait: false,
                    //               propagate: false,
                    //               parameters: [
                    //                   string(name: 'GIT_SHA', value: env.GIT_COMMIT.take(7))
                    //               ]
                    //     }
                    // }

                    if (fanout.isEmpty()) {
                        echo '변경된 디렉터리가 없습니다 – 빌드 종료'
                    } else {
                        parallel fanout
                    }
                }
            }
        }
    }

    post {
        success { echo 'Root 파이프라인 정상 종료 (Child Jobs 비동기 실행 중)' }
        failure { echo 'Root 파이프라인 실패 – 콘솔 로그 확인' }
    }
}
