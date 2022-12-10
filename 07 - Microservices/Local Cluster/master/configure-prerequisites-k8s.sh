#!/bin/sh
echo '======================================================================================='
echo 'ifconfig'
echo '======================================================================================='

echo 'sudo ifconfig'
echo "$(sudo ifconfig)"

echo '======================================================================================='
echo 'Disable swap, swapoff then edit your fstab removing any entry for swap partitions'
echo 'You can recover the space with fdisk. You may want to reboot to ensure your config is ok.'
echo '======================================================================================='

echo "sudo swapoff -a"
echo "$(sudo swapoff -a)"

#echo "sudo vi /etc/fstab"
#echo "$(sudo vi /etc/fstab)"

echo '======================================================================================='
echo 'Add Google apt repository gpg key'
echo '======================================================================================='

echo "curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -"
echo "$(curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -)"

echo '======================================================================================='
echo 'Add the Kubernetes apt repository'
echo '======================================================================================='

echo 'sudo cat <<EOF >/etc/apt/sources.list.d/kubernetes.list'
sudo cat <<EOF >/etc/apt/sources.list.d/kubernetes.list
deb https://apt.kubernetes.io/ kubernetes-xenial main
EOF

echo '======================================================================================='
echo 'Update the package list and use apt-cache to inspect versions available in the repository'
echo '======================================================================================='

echo "sudo apt-get update"
echo "$(sudo apt-get update)"

echo "sudo apt-cache policy kubelet | head -n 20 "
echo "$(sudo apt-cache policy kubelet | head -n 20 )"

echo "sudo apt-cache policy docker.io | head -n 20 "
echo "$(sudo apt-cache policy docker.io | head -n 20 )"

echo '======================================================================================='
echo 'Install the required packages, if needed we can request a specific version'
echo '======================================================================================='

echo "sudo apt-get install -y docker.io kubelet kubeadm kubectl"
echo "$(sudo apt-get install -y docker.io kubelet kubeadm kubectl)"

echo "sudo apt-mark hold docker.io kubelet kubeadm kubectl"
echo "$(sudo apt-mark hold docker.io kubelet kubeadm kubectl)"

echo '======================================================================================='
echo 'Check the status of our kubelet and our container runtime, docker.'
echo 'The kubelet will enter a crashloop until it''s joined.'
echo '======================================================================================='

echo "sudo systemctl status kubelet.service"
echo "$(sudo systemctl status kubelet.service )"

echo "sudo systemctl status docker.service"
echo "$(sudo systemctl status docker.service )"

echo '======================================================================================='
echo 'Ensure both are set to start when the system starts up.'
echo '======================================================================================='

echo "sudo systemctl enable kubelet.service"
echo "$(sudo systemctl enable kubelet.service)"

echo "sudo systemctl enable docker.service"
echo "$(sudo systemctl enable docker.service)"

echo '======================================================================================='
echo 'Setup Docker daemon.'
echo '======================================================================================='

sudo cat <<EOF > /etc/docker/daemon.json 
{
  "exec-opts": ["native.cgroupdriver=systemd"],
  "log-driver": "json-file",
  "log-opts": {
    "max-size": "100m"
  },
  "storage-driver": "overlay2"
}
EOF

echo '======================================================================================='
echo 'Restart reload the systemd config and docker'
echo '======================================================================================='

echo "sudo systemctl daemon-reload"
echo "$(sudo systemctl daemon-reload)"

echo "sudo systemctl restart docker"
echo "$(sudo systemctl restart docker)"

# Be sure NOT to execute "netplan apply" here, so the changes take effect on
# reboot instead of immediately, which would disconnect the provisioner.