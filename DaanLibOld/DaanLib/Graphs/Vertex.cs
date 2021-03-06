﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaanLib.Graphs {
    /// <summary>
    /// Represents a point on a Graph
    /// </summary>
    public sealed class Vertex : IEquatable<Vertex> {
        /// <summary>
        /// An id setter used to set the ID of the Vertex
        /// </summary>
        private static readonly IDSetter idSetter = new IDSetter();

        /// <summary>
        /// The unique ID of the vertex
        /// </summary>
        public readonly int id;

        /// <summary>
        /// The name of the vertex
        /// </summary>
        public readonly string name;
        /// <summary>
        /// The list of edges this Vertex is linked with
        /// </summary>
        public readonly List<Edge> edgeList;

        /// <summary>
        /// The degree of this Vertex
        /// </summary>
        public int degree => edgeList.Count;

        /// <summary>
        /// Instantiates a new Vertex
        /// </summary>
        /// <param name="name">The name of the Vertex</param>
        public Vertex(string name) {
            id = idSetter.getNextValidId;

            this.name = name;
            edgeList = new List<Edge>();
        }

        /// <summary>
        /// Adds an Edge to this Vertex
        /// </summary>
        /// <param name="edge">The Edge to add</param>
        /// <exception cref="ArgumentException">Throws an Argument Exception if the given edge is null</exception>
        /// <returns>Whether the Edge was succesfully added</returns>
        public bool AddEdge(Edge edge) {
            if (edge == null)
                throw new ArgumentException("Edge cannot be null");

            if (edgeList.Contains(edge))
                return false;

            edgeList.Add(edge);

            return true;
        }

        /// <summary>
        /// Adds an Edge to this Vertex that connects it to the destination Vertex
        /// </summary>
        /// <param name="destination">The Vertex this edge should connect with</param>
        /// <param name="cost">The cost to travel to the destination (Defaults to 1.0f)</param>
        /// <returns>Whether the Edge was succesfully added</returns>
        public bool AddEdge(Vertex destination, float cost = 1.0f) {
            if (destination == null)
                throw new ArgumentException("Destination Vertex cannot be null");

            var edge = new Edge(this, destination, cost);

            return AddEdge(edge);
        }

        /// <summary>
        /// Checks whether this Vertex and another object are equal
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>True if these objects are equal</returns>
        public override bool Equals(object obj) => Equals(obj as Vertex);
        /// <summary>
        /// Checks whether this Vertex and another Vertex are equal
        /// </summary>
        /// <param name="other">The other Vertex to compare to</param>
        /// <returns>True if these objects are equal</returns>
        public bool Equals(Vertex other) => other != null && id == other.id && name == other.name;

        /// <summary>
        /// Calculates this Vertex' hash code
        /// </summary>
        /// <returns>The hash code of this Vertex</returns>
        public override int GetHashCode() {
            int hashCode = -48284730;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            return hashCode;
        }

        /// <summary>
        /// Compares two Verteces to eachother to see if they are equal to eachother
        /// </summary>
        /// <param name="left">The left side Vertex</param>
        /// <param name="right">The right side Vertex</param>
        /// <returns>True if both Verteces are equal to eachother</returns>
        public static bool operator ==(Vertex left, Vertex right) => EqualityComparer<Vertex>.Default.Equals(left, right);
        /// <summary>
        /// Compares two Verteces to eachother to see if they are not equal to eachother
        /// </summary>
        /// <param name="left">The left side Vertex</param>
        /// <param name="right">The right side Vertex</param>
        /// <returns>True if both Verteces are not equal to eachother</returns>
        public static bool operator !=(Vertex left, Vertex right) => !(left == right);
    }
}
