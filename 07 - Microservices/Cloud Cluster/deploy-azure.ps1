echo "######################################################################################################"
echo "##                                                                                                  ##"
echo "## Configure manually before running                                                                ##"
echo "## az login                                                                                         ##"
echo "## az account set --subscription=""""SUBSCRIPTION_ID""""                                                ##"
echo "## az ad sp create-for-rbac --role=""""Contributor""""                                                  ##"
echo "## az ad sp create-for-rbac --role=""""Contributor"""" --scopes=""""/subscriptions/SUBSCRIPTION_ID""""      ##"
echo "## az vm image accept-terms --urn cognosys:centos-8-stream-free:centos-8-stream-free:1.2019.0810    ##"
echo "##                                                                                                  ##"
echo "######################################################################################################"

cd .\k8s\terraform\03
terraform init
terraform apply --auto-approve
cd ..\..\..\

ssh-keygen -R vmip-configurator-host-ms-publicip.westeurope.cloudapp.azure.com
ssh -o StrictHostKeyChecking=no -I .\k8s\terraform\03\id_rsa adminUser@vmip-configurator-host-ms-publicip.westeurope.cloudapp.azure.com ansible-playbook -l all ansible/00-run-all.yaml
#ssh -o StrictHostKeyChecking=no -I .\k8s\terraform\03\id_rsa adminUser@vmip-configurator-host-ms-publicip.westeurope.cloudapp.azure.com
#sh configure-vm.sh '10.0.1.9' 'configurator-host' 'conf' 'adminUser' 'P@ssword1234'

echo "Configure ip master on host"
Set-Variable -Name "ipMaster" -Value $(az vm show -d -g rg-kubernetes-ms -n k8s-c8-master --query publicIps -o tsv)
.\Set-HostsEntry -IPAddress $ipMaster -HostName "game.bar" -Path "C:\Windows\System32\drivers\etc\hosts"

Set-Variable -Name "nodePort" -Value $(ssh -o StrictHostKeyChecking=no -I .\k8s\terraform\03\id_rsa adminUser@vmip-configurator-host-ms-publicip.westeurope.cloudapp.azure.com "kubectl get svc -n=haproxy-controller -o go-template='{{range .items}}{{range.spec.ports}}{{if .nodePort}}{{.nodePort}}{{""""\n""""}}{{end}}{{end}}{{end}}'").Split("\n")[0]
Set-Variable -Name "urlGame" -Value "http://game.bar:$nodePort"

echo "####################################################"
echo "## Test url = http://game.bar:$nodePort               ##"
echo "####################################################"