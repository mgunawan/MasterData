#MasterData Service

#Test Service
1. gcloud container clusters get-credentials vini-dev-00 --zone asia-southeast1-c --project vini-dev-1902
2. kubectl port-forward deployment/masterdata 4022:4022 -n bb-core
3. curl http://127.0.0.1:4022/v1/area