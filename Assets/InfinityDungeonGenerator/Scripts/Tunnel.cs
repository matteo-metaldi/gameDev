using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : IComparable<Tunnel> {
	public int tunnelCounter { get; set; }
	public GameObject tunnelPart { get; set; }
	// Use this for initialization

	public Tunnel(int tunnelCounter, GameObject tunnelPart)
	{
		this.tunnelCounter = tunnelCounter;
		this.tunnelPart = tunnelPart;
	}

	public void findAllTunnels()
	{
		
	}
	
	public int CompareTo(Tunnel other)
	{
		if(other == null)
		{
			return 1;
		}
        
		//Return the difference in power.
		return tunnelCounter - other.tunnelCounter;
	}
}
