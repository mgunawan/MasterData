pipeline {
    agent {
        node {
            label 'slave-00'
            customWorkspace "workspace/${env.BRANCH_NAME}/src/git.bluebird.id/bb-core/MasterData"
        }
    }
    environment {
        VERSION = '1.0.0'
    }
    options {
        buildDiscarder(logRotator(daysToKeepStr: env.BRANCH_NAME == 'master' ? '90' : '30'))
    }
    stages {
        stage('Checkout') {
            when {
                anyOf { branch 'master'; branch 'develop'; branch 'staging' }
            }
            steps {
                echo 'Checking out from Git'
                checkout scm
            }
        }
    }
}
