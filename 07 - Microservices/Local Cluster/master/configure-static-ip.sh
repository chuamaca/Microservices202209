#!/bin/sh

echo '======================================================================================='
echo 'Install -tools'
echo '======================================================================================='

echo "sudo apt install net-tools"
echo "$(sudo apt install net-tools)"

echo '======================================================================================='
echo 'Setting static IP address for Hyper-V...'
echo '======================================================================================='

sudo cat << EOF > /etc/netplan/01-netcfg.yaml
network:
  version: 2
  ethernets:
    eth0:
      dhcp4: no
      addresses: [192.168.1.110/24]
      gateway4: 192.168.1.1
      nameservers:
        addresses: [8.8.8.8,8.8.4.4]
EOF

echo '======================================================================================='
echo 'Setting hosts for Hyper-V...'
echo '======================================================================================='

sudo cat << EOF > /etc/hosts
127.0.0.1       localhost
127.0.1.1       k8s-master      k8s-master
192.168.1.110   k8s-master      k8s-master
192.168.1.111	k8s-node01      k8s-node01
192.168.1.112	k8s-node02      k8s-node02

# The following lines are desirable for IPv6 capable hosts
::1     localhost ip6-localhost ip6-loopback
ff02::1 ip6-allnodes
ff02::2 ip6-allrouters

127.0.0.1 ubuntu2004.localdomain
EOF

echo '======================================================================================='
echo 'Restart networking Hyper-V...'
echo '======================================================================================='

echo "sudo systemctl restart networking"
echo "$(sudo systemctl restart networking)"

# Be sure NOT to execute "netplan apply" here, so the changes take effect on
# reboot instead of immediately, which would disconnect the provisioner.
