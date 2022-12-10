#!/bin/sh

echo '======================================================================================='
echo 'Look inside calico.yaml and find the network range CALICO_IPV4POOL_CIDR, adjust if needed.'
echo '======================================================================================='

#echo "rm -r calico.*"
#echo "$(rm -r calico.*)"

#echo "sudo systemctl stop kubelet"
#echo "$(sudo systemctl stop kubelet)"

#echo "sudo rm -r /etc/cni/net.d/*"
#echo "$(sudo rm -r /etc/cni/net.d/*)"

echo "sudo rm -f $HOME/.kube/config"
echo "$(sudo rm -f $HOME/.kube/config)"

echo "sudo kubeadm reset -f"
echo "$(sudo kubeadm reset -f)"

echo "ls"
echo "$(ls)"

echo '======================================================================================='
echo 'Create our kubernetes cluster, specifying a pod network range matching that in calico.yaml!'
echo '======================================================================================='

echo "sudo kubeadm init --pod-network-cidr=192.168.0.0/16"
echo "$(sudo kubeadm init --pod-network-cidr=192.168.0.0/16)"

echo '======================================================================================='
echo 'Configure our account on the master to have admin access to the API server from a non-privileged account.'
echo '======================================================================================='

echo "sudo mkdir -p $HOME/.kube"
echo "$(sudo mkdir -p $HOME/.kube)"

echo "sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config"
echo "$(sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config)"

echo "sudo chown $(id -u):$(id -g) $HOME/.kube/config"
echo "$(sudo chown $(id -u):$(id -g) $HOME/.kube/config)"

echo '======================================================================================='
echo 'Deploy yaml file for your pod network.'
echo 'This line of code has be updated since the publication of the course.'
echo '======================================================================================='

echo "sudo kubectl apply -f calico.yaml"
echo "$(sudo kubectl apply -f calico.yaml)"

echo '======================================================================================='
echo 'Look for the all the system pods and calico pod to change to Running.'
echo 'The DNS pod won''t start until the Pod network is deployed and Running.'
echo '======================================================================================='

echo "sudo kubectl get pods --all-namespaces"
echo "$(sudo kubectl get pods --all-namespaces)"

echo '======================================================================================='
echo 'Gives you output over time, rather than repainting the screen on each iteration.'
echo '======================================================================================='

echo "sudo kubectl get pods --all-namespaces --watch"
#echo "$(sudo kubectl get pods --all-namespaces --watch)"

echo '======================================================================================='
echo 'All system pods should be Running'
echo '======================================================================================='

echo "sudo kubectl get pods --all-namespaces"
echo "$(sudo kubectl get pods --all-namespaces)"

echo '======================================================================================='
echo 'Get a list of our current nodes, just the master.'
echo '======================================================================================='

echo "sudo kubectl get nodes"
echo "$(sudo kubectl get nodes )"

echo '======================================================================================='
echo 'Check out the systemd unit, and examine 10-kubeadm.conf'
echo 'Remeber the kubelet starts static pod manifests, and thus the core cluster pods'
echo '======================================================================================='

echo "sudo systemctl status kubelet.service"
echo "$(sudo systemctl status kubelet.service )"

echo '======================================================================================='
echo 'check out the directory where the kubeconfig files live'
echo '======================================================================================='

echo "sudo ls /etc/kubernetes"
echo "$(sudo ls /etc/kubernetes)"

echo '======================================================================================='
echo 'let''s check out the manifests on the master'
echo '======================================================================================='

echo "sudo ls /etc/kubernetes/manifests"
echo "$(sudo ls /etc/kubernetes/manifests)"

echo '======================================================================================='
echo 'And look more closely at API server and etcd''s manifest.'
echo '======================================================================================='

echo "sudo more /etc/kubernetes/manifests/etcd.yaml"
echo "$(sudo more /etc/kubernetes/manifests/etcd.yaml)"

echo "sudo more /etc/kubernetes/manifests/kube-apiserver.yaml"
echo "$(sudo more /etc/kubernetes/manifests/kube-apiserver.yaml)"

# Be sure NOT to execute "netplan apply" here, so the changes take effect on
# reboot instead of immediately, which would disconnect the provisioner.