using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetwork;
using UnityEngine.UI;
using System.Threading;

public class NetLayer : MonoBehaviour {

	//Neural Network Variables
	private const double MinimumError = 0.1;
	private const TrainingType TrType = TrainingType.MinimumError;
	private static NeuralNet net;
	public static List<DataSet> dataSets; 
	public static bool trained;

	private int collectedDatasets = 0;
	private const int maxNumberOfDatasets = 60; 
	public CsvReadWrite csv;

	public Player player; 

	public Text distanceText;

	// Use this for initialization
	void Start () {
		//Initialize the network 
		net = new NeuralNet(2, 3, 1);
		dataSets = new List<DataSet>();
		trained = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Let the network decide if the player should jump
		if (trained) {
			double result = compute(new double[] { player.distanceInPercent, player.canJump }) ;
			//csv.SaveDouble(result, "Result: ");

			if (result > 0.5) {
				csv.SaveDouble(result,"Result: ",2);				
				distanceText.text = result.ToString();
				//Debug.Log(result);

				player.jump ();
				for (int i = 0; i < net.HiddenLayers.Count; i++)
				{
					for (int j = 0; j < net.HiddenLayers[i].Count; j++)
					{
						csv.SaveDouble(net.HiddenLayers[i][j].Bias, "Bias: ",3);
						csv.SaveDouble(net.HiddenLayers[i][j].Gradient, "Gradient: ",4);
						csv.SaveDouble(net.HiddenLayers[i][j].Value, "Value: ", 5);
					}
				}

			}
		}
	}

	public void Train(double canJump, double jumped)
	{ 
		double[] C = {player.distanceInPercent, canJump};
		double[] v = {jumped};
		dataSets.Add(new DataSet(C, v));
		//this is where to pass in values for output
		csv.SaveDistance(C);
		collectedDatasets++;
		if (!trained && collectedDatasets == maxNumberOfDatasets) {
			print ("Start training of the network."); 
			TrainNetwork();
			//IsTrained();
            csv.addSingleRow = true;
		}
	}

	double compute(double[] vals)
	{
		double[] result = net.Compute(vals);
		return result[0];
	}

	private void IsTrained()
    {
		csv.AddSingleRown();
    }

	public static void TrainNetwork()
	{
		net.Train(dataSets, MinimumError);
		trained = true;
		print ("Trained!"); 
		Debug.Log("trained");
	}
}
