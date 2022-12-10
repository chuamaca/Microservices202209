#!/bin/sh

echo '======================================================================================='
echo 'If you didn''t keep the output, on the master, you can get the token.'
echo '======================================================================================='

echo "sudo kubeadm token list <----- Ejecutar en el master"
#echo "$(sudo kubeadm token list)"

echo '======================================================================================='
echo 'If you need to generate a new token, perhaps the old one timed out/expired.'
echo '======================================================================================='

echo "sudo kubeadm token create  <----- Ejecutar en el master [TOKEN]"
#echo "$(sudo kubeadm token create)"

echo '======================================================================================='
echo 'On the master, you can find the ca cert hash.'
echo '======================================================================================='

echo "sudo openssl x509 -pubkey -in /etc/kubernetes/pki/ca.crt | openssl rsa -pubin -outform der 2>/dev/null | openssl dgst -sha256 -hex | sed 's/^.* //' <-------- ejecutar en el master [CERTIFICADO]"
#echo "$(sudo openssl x509 -pubkey -in /etc/kubernetes/pki/ca.crt | openssl rsa -pubin -outform der 2>/dev/null | openssl dgst -sha256 -hex | sed 's/^.* //')"

echo '======================================================================================='
echo 'Using the master (API Server) IP address or name, the token and the cert has, let''s join this Node to our cluster.'
echo '======================================================================================='

#sudo kubeadm join [IP_MASTER]:6443 \
#	--token [TOKEN] \
#	--discovery-token-ca-cert-hash sha256:[CERTIFICADO]    
	
echo "sudo kubeadm join [IP_MASTER]:6443 \\"
echo "	--token [TOKEN] \\"
echo "	--discovery-token-ca-cert-hash sha256:[CERTIFICADO]"

sudo kubeadm join 192.168.1.110:6443 \
    --token zwidn5.i72j94rf2o2nszyf \
    --discovery-token-ca-cert-hash sha256:6c4fa7c2901ee4e77025f5f17ddd6334bac329dee751d314f7dc45ec08fe694e

echo '======================================================================================='
echo 'Back on master, this will say NotReady until the networking pod is created on the new node. Has to schedule the pod, then pull the container.'
echo '======================================================================================='

echo "sudo kubectl get nodes <------  Ejecutar en el master"
#echo "$(sudo kubectl get nodes)"

echo '======================================================================================='
echo 'On the master, watch for the calico pod and the kube-proxy to change to Running on the newly added nodes.'
echo '======================================================================================='

echo "sudo kubectl get pods --all-namespaces --watch <------  Ejecutar en el master"
#echo "$(sudo kubectl get pods --all-namespaces --watch)"

echo '======================================================================================='
echo 'Still on the master, look for this added node''s status as ready.'
echo '======================================================================================='

echo "sudo kubectl get nodes <------  Ejecutar en el master"
#echo "$(sudo kubectl get nodes)"

echo '======================================================================================='
echo 'GO BACK TO THE TOP AND DO THE SAME FOR c1-node2.'
echo 'Just SSH into c1-node2 and run the commands again.'
echo '======================================================================================='

#You can skip the token re-creation if you have one that's still valid.

# Be sure NOT to execute "netplan apply" here, so the changes take effect on
# reboot instead of immediately, which would disconnect the provisioner.